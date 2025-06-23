using BlazorTecnicalTest.Models.Task;
using BlazorTecnicalTest.Repositories;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazorTecnicalTest.Logic
{
    public partial class TaskForm : ComponentBase
    {

        [Parameter] public string? Id { get; set; }
        public bool IsEdit => Id != null;
        public RegistrarTarea formModel;
        protected string mensaje;

        [Inject] public TaskRepositorie TaskRepositorie { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            formModel = new RegistrarTarea(); // ✅ Inicializar al principio

            if (IsEdit)
            {
                var existente = await TaskRepositorie.GetByIdAsync(Id!);
                if (existente != null)
                {
                    formModel.Titulo = existente.Titulo;
                    formModel.Descripcion = existente.Descripcion;
                    formModel.Estado = existente.Estado;
                }
            }
        }


        protected async Task Guardar()
        {


            if (IsEdit)

            {
                var tarea = new TaskItem
                {
                    Id = Id, 
                    Titulo = formModel.Titulo,
                    Descripcion = formModel.Descripcion,
                    Estado = formModel.Estado
                };
                await TaskRepositorie.UpdateAsync(Id!, tarea);

            }
            else
            {
                var tarea = new TaskItem
                {
                    Titulo = formModel.Titulo,
                    Descripcion = formModel.Descripcion,
                    Estado = formModel.Estado
                };
                await TaskRepositorie.CreateAsync(tarea);


            }

            Navigation.NavigateTo("/tareas", true); 
        }

        public class RegistrarTarea
        {
            [Required(ErrorMessage = "El título es obligatorio.")]
            public string Titulo { get; set; } = "";

            [Required(ErrorMessage = "La descripción es obligatoria.")]
            public string Descripcion { get; set; } = "";

            [Required(ErrorMessage = "El estado es obligatorio.")]
            public string Estado { get; set; } = "";
        }

    }
}
