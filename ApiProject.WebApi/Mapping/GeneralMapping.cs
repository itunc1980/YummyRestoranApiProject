using ApiProject.WebApi.Dtos.AboutDtos;
using ApiProject.WebApi.Dtos.CategoryDtos;
using ApiProject.WebApi.Dtos.FeatureDtos;
using ApiProject.WebApi.Dtos.MessageDtos;
using ApiProject.WebApi.Dtos.NotificationDtos;
using ApiProject.WebApi.Dtos.ProductDtos;
using ApiProject.WebApi.Entities;
using AutoMapper;

namespace ApiProject.WebApi.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();

        //AutoMapper'deki ReverseMap() metodu, iki yönlü dönüşüm sağlar. Yani, bir nesneyi diğerine dönüştürdüğünüz gibi, tersini de otomatik olarak yapmanıza olanak tanır.
        // Özetle:
        // ✔ Tek satırla çift yönlü mapleme sağlar.
        // ✔ Kod tekrarını azaltır.
        // ✔ Kolay kullanım sunar.

            CreateMap<Message, ResultMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, GetByIdMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();

            CreateMap<Product,CreateProductDto>().ReverseMap();
            CreateMap<Product, ResultProductWithCategoryDto>().ForMember(x => x.CategoryName, y => y.MapFrom(z => z.Category.CategoryName)).ReverseMap();

            CreateMap<Notification, ResultNotificationDto>().ReverseMap();
            CreateMap<Notification, CreateNotificationDto>().ReverseMap();
            CreateMap<Notification, GetByIdNotificationDto>().ReverseMap();
            CreateMap<Notification, UpdateNotificationDto>().ReverseMap();

            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, GetAboutByIdDto>().ReverseMap();
            CreateMap<About, ResultAboutDto>().ReverseMap();

        }
    }
}
