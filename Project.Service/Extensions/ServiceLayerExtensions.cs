using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Project.Service.Services.Concrete;
using Project.Service.Services.Abstract;
using Microsoft.Extensions.FileProviders;

namespace Project.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        /// <summary>
        /// Automapper ve Automapper.Extensions.Microsoft.DependencyInjection paketleri kullanılır.
        /// Program.cs içerisinden değil de bu dosya içerisinden daha derli toplu bir servis ekleme yoludur.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            //services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
            services.AddScoped<UserAccountService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<UserProfileService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<IShopService,ShopService>();
            services.AddScoped<CommentService>();
            services.AddScoped<IContactUsService,ContactUsService>();
            #region FluentValidation
            services.AddFluentValidationAutoValidation(opt =>
            {
                opt.DisableDataAnnotationsValidation = true;
            }).AddFluentValidationClientsideAdapters();
            //services.AddValidatorsFromAssemblyContaining<AddressValidator>();
            #endregion

            #region Localization
            services.AddSingleton<LanguageService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                    return factory.Create(nameof(SharedResource), assemblyName.Name);
                });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("tr-TR"),
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
            });
            #endregion

            return services;
        }
    }
}
