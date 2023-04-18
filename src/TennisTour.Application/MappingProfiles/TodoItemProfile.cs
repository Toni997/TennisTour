using AutoMapper;
using TennisTour.Application.Models.TodoItem;
using TennisTour.Core.Entities;

namespace TennisTour.Application.MappingProfiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<CreateTodoItemModel, TodoItem>()
            .ForMember(ti => ti.IsDone, ti => ti.MapFrom(cti => false));

        CreateMap<UpdateTodoItemModel, TodoItem>();

        CreateMap<TodoItem, TodoItemResponseModel>();
    }
}
