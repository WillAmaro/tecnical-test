
using BlazorTecnicalTest.Models.User;
using BlazorTecnicalTest.Repositories;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;


namespace BlazorTecnicalTest.Logic;

public partial class Register : ComponentBase
{
    //public RegistroUsuario formModel = new();
    protected string mensaje;
    [Inject] public UserRepository UsuarioRepo { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }

    public RegistroUsuario formModel;

    protected override void OnInitialized()
    {
        formModel = new RegistroUsuario();
    }



    protected async Task HandleValidSubmit()
    {
        try
        {
            if (await UsuarioRepo.ExistByEmail(formModel.Email))
            {
                formModel = new RegistroUsuario();
                return;
            }

            var usuario = new Usuario
            {
                Nombre = formModel.NombreCompleto,
                Email = formModel.Email,
                PasswordHash = HashearPassword(formModel.Password)
            };

            await UsuarioRepo.CrearAsync(usuario);

            Navigation.NavigateTo("/usuarios");
            formModel = new RegistroUsuario();

        }
        catch
        {
            formModel = new RegistroUsuario();

        }
    }

    protected string HashearPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public class RegistroUsuario
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string NombreCompleto { get; set; } = "";

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo inválido.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "Mínimo 6 caracteres.")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirma tu contraseña.")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarPassword { get; set; } = "";
    }
}