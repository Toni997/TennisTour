using AutoMapper;
using TennisTour.Application.Models.TodoList;
using TennisTour.Core.Entities;

namespace TennisTour.Application.MappingProfiles;

public class TodoListProfile : Profile
{
    public TodoListProfile()
    {
        CreateMap<CreateTodoListModel, TodoList>();

        CreateMap<TodoList, TodoListResponseModel>();
    }
}
