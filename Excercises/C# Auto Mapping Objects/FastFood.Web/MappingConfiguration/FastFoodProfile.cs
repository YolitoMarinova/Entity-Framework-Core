namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Web.ViewModels.Categories;
    using FastFood.Web.ViewModels.Employees;
    using FastFood.Web.ViewModels.Items;
    using FastFood.Web.ViewModels.Orders;
    using Models;

    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<Position, RegisterEmployeeViewModel>()
                  .ForMember(x => x.PositionId, y => y.MapFrom(x => x.Id))
                  .ForMember(x => x.PositionName, y => y.MapFrom(x => x.Name));

            //Employees
            this.CreateMap<RegisterEmployeeInputModel, Employee>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.Age, y => y.MapFrom(x => x.Age))
                .ForMember(x => x.PositionId, y => y.MapFrom(x => x.PositionId))
                .ForMember(x => x.Address, y => y.MapFrom(x => x.Address));

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.Age, y => y.MapFrom(x => x.Age))
                .ForMember(x => x.Position, y => y.MapFrom(x => x.Position.Name))
                .ForMember(x => x.Address, y => y.MapFrom(x => x.Address));


            //Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.CategoryName));

            //Тук се вижда, че и когато пропъртита са еднакви, не е нужно да казваме на мапъра изрично кое с кое да мапне.
            this.CreateMap<Category, CategoryAllViewModel>();

            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.CategoryName, y => y.MapFrom(x => x.Name));            

            //Items
            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y => y.MapFrom(x => x.Category.Name));


            //Orders
            this.CreateMap<CreateOrderInputModel, Order>()
                .ForMember(x => x.EmployeeId, y => y.MapFrom(x => x.EmployeeId));

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.Employee, y => y.MapFrom(x => x.Employee.Name))
                .ForMember(x => x.OrderId, y => y.MapFrom(x =>x.Id));

            this.CreateMap<CreateOrderInputModel, OrderItem>()
                .ForMember(x => x.ItemId, y => y.MapFrom(x => x.ItemId))
                .ForMember(x => x.Quantity, y => y.MapFrom(x => x.Quantity));

        }
    }
}
