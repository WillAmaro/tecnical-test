using BlazorTecnicalTest.Models.Task;
using BlazorTecnicalTest.Repositories;
using Microsoft.AspNetCore.Components;

namespace BlazorTecnicalTest.Logic
{
       public class TaskListBase : ComponentBase
    {
        [Inject] public TaskRepositorie TaskRepositorie { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        protected List<TaskItem>? tareas;

        protected int CurrentPage { get; set; } = 1;
        protected int PageSize { get; set; } = 5;

        protected IEnumerable<TaskItem> PagedTareas => tareas?
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize) ?? Enumerable.Empty<TaskItem>();

        protected int TotalPages => (int)Math.Ceiling((double)(tareas?.Count ?? 0) / PageSize);

        protected override async Task OnInitializedAsync()
        {
            tareas = await TaskRepositorie.GetAllAsync();
        }

        protected void Edit(string id) => Navigation.NavigateTo($"/registrar-tarea/{id}");
        protected void Create() => Navigation.NavigateTo($"/registrar-tarea");

        protected async Task Delete(string id)
        {
            await TaskRepositorie.DeleteAsync(id);
            tareas = await TaskRepositorie.GetAllAsync();
            if (CurrentPage > TotalPages) CurrentPage = TotalPages;
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