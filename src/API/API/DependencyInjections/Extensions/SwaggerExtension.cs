using Microsoft.OpenApi.Models;
using CleanArchitectureTemplate.API.DependencyInjections.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitectureTemplate.API.DependencyInjections
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Add Swagger (OpenAPI) documentation configurations
        /// </summary>
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();
                
                // Include Areas
                foreach (var area in AreaExtension.GetAreas())
                    c.SwaggerDoc(area, new OpenApiInfo { Title = $"{area} APIs", Version = "v1" });

                c.TagActionsBy(api => [$"{api.ActionDescriptor.RouteValues["controller"]}" ?? "Default"]);
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var area = $"{apiDesc.ActionDescriptor.RouteValues["area"]}";
                    return !string.IsNullOrEmpty(area) && docName.Equals(area, StringComparison.OrdinalIgnoreCase);
                });

                // Include XML comments if you have them.
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: false);

                // Configure OAuth2 scheme
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityServer:Authority")}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "Identity", "Access to Identity" },
                                { "CleanArchitectureTemplate", "Access to Clean Architecture Template" }
                            },
                        }
                    }
                });

                // Add Security Requirement using the OAuth2 scheme
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2",
                            }
                        },
                        new List<string> { "Identity", "CleanArchitectureTemplate" }
                    }
                });

                c.ParameterFilter<DynamicParameterDescriptionFilter>();
                c.SchemaFilter<DynamicEnumSchemaFilter>();
            });
        }

        /// <summary>
        /// Use Swagger Middleware Injection
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwagger(this WebApplication app)
        {
            if (app.Environment.IsProduction())
                return;

            SwaggerBuilderExtensions.UseSwagger(app);
            app.UseSwaggerUI(c =>
            {
                // Add Swagger UI endpoints for each controller
                foreach (var area in AreaExtension.GetAreas())
                {
                    c.SwaggerEndpoint($"{area}/swagger.json", $"{area} APIs v1");
                    c.OAuthClientId("cleanarchitecturetemplate_portal");
                    c.OAuthScopes("Identity", "CleanArchitectureTemplate");
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                }
            });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DynamicParameterDescriptionFilter : IParameterFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            try
            {
                var paramType = context.ParameterInfo?.ParameterType ?? context.PropertyInfo?.PropertyType;
                if (paramType != null && paramType.IsEnum)
                {
                    var enumNames = Enum.GetNames(paramType);
                    var enumValues = Enum.GetValues(paramType).Cast<int>();

                    var enumDescriptions = enumNames.Zip(enumValues, (name, value) => $"{name} = {value}");
                    parameter.Description = $"{string.Join(", ", enumDescriptions)}.";
                }
            }
            catch (Exception)
            {

            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DynamicEnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            try
            {
                var type = context.Type;
                if (type.IsEnum)
                {
                    var enumNames = Enum.GetNames(type);
                    var enumValues = Enum.GetValues(type).Cast<int>();

                    var enumDescriptions = enumNames.Zip(enumValues, (name, value) => $"{name} = {value}");
                    schema.Description += $"{string.Join(", ", enumDescriptions)}.";
                }

                // Check if this schema has enum properties
                foreach (var property in schema.Properties)
                {
                    var propertyType = context.Type.GetProperty(property.Key)?.PropertyType;
                    if (propertyType != null && propertyType.IsEnum)
                    {
                        var propertyEnumNames = Enum.GetNames(propertyType);
                        var propertyEnumValues = Enum.GetValues(propertyType).Cast<int>();

                        var propertyEnumDescriptions = propertyEnumNames.Zip(propertyEnumValues, (name, value) => $"{name} = {value}");
                        schema.Properties[property.Key].Description += $"{string.Join(", ", propertyEnumDescriptions)}.";
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
