using BlazorTecnicalTest.Models.Task;
using BlazorTecnicalTest.Models.User;
using BlazorTecnicalTest.Repositories;
using Microsoft.AspNetCore.Components;
using System.Threading;
using TenicalTest.Components.Pages;

namespace BlazorTecnicalTest.Logic
{
    public class UserListBase : ComponentBase
    {
        [Inject] public UserRepository UserRepository { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

  


        protected List<Usuario>? usuarios;

        protected int CurrentPage { get; set; } = 1;
        protected int PageSize { get; set; } = 5;

        protected IEnumerable<Usuario> PagedUsuarios => usuarios?
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize) ?? Enumerable.Empty<Usuario>();

        protected int TotalPages => (int)Math.Ceiling((double)(usuarios?.Count ?? 0) / PageSize);
        protected override async Task OnInitializedAsync()
        {
            usuarios = await UserRepository.GetAllAsync();
        }



        protected void Crear()
        {
            Navigation.NavigateTo($"/registro-usuario");
        }

        protected void ChangePage(int nuevaPagina)
        {
            if (nuevaPagina >= 1 && nuevaPagina <= TotalPages)
            {
                CurrentPage = nuevaPagina;
                StateHasChanged();

            }
        }
    }
}