# 063_CSharpPatrones

- IAMTIMCOREY: PATRONES
	- Carpeta relacionada: 063_CSharpPatrones
	- Factory Pattern in C# with Dependency Injection
		- https://www.youtube.com/watch?v=2PXAfSfvRKY
			
		- Add new project
			- Blazor server app
			- Framework: .NET 6.0
			- Authentication type: None
			- Configure for HTTPS
			- Do not use top-level statements
			
		- Primer acercamiento
			- Mostrar fecha y hora
				- Creamos una nueva clase que devuelva la fecha y hora actual
				- La desplegaremos en una nueva página.
				- Al cambiar de una página y volver podemos ver que la hora se actualiza.
				
			- Tener una instancia de ISample_001 más de una vez en la misma página.
				- Lo que se puede hacer es poner un botón y que este tenga una nueva instancia; pero vemos que únicamente sirve la primera vez que se presiona el botón, esto es debido a que tenemos la misma instancia del objeto sample
				
					```cs
						@page "/factory"
						@*@using Patrons.Samples.Factory*@
						
						@inject ISample_001 sample
						
						<PageTitle>Factory</PageTitle>
						
						<h1>Factory Pattern</h1>
						
						<h2>@currentTime?.CurrentDateTime</h2>
						
						<button class="btn btn-primary" @onclick="GetNewTime">Get New Time</button>
						
						@code {
							ISample_001? currentTime;
							private void GetNewTime(){
								currentTime = sample;
							}
						}
					```
				- Si hacemos una instanciación MANUAL podemos ver que SI FUNCIONA pero el problema es que se está mezclando DI y no DI lo que no se debe hacer.
				
					```cs
						@page "/factory"					
						@inject ISample_001 sample
						
						....
						
						@code {
							ISample_001? currentTime;
							private void GetNewTime(){
								currentTime = new Sample();
							}
						}
					```

			- Obtener nuevas instancias USANDO DI adecuadamente usando el patrón factory.
				- La versión MAS SIMPLE del patrón factory. 
					- En program.cs inyectamos una función que nos devolvera la fecha.
				
						```cs						
							namespace Patrons
							{
								public class Program
								{
									public static void Main(string[] args)
									{
										....
							
										builder.Services.AddTransient<ISample_001, Sample_001>();
										builder.Services.AddSingleton<Func<ISample_001>>(x => () => x.GetService<ISample_001>());
							
										....
									}
								}
							}
						```
						
					- Modificamos la vista.
				
						```cs						
							....
							
							@inject Func<ISample_001> factory
							
							....
							
							@code {
								ISample_001? currentTime;
								private void GetNewTime(){
									currentTime = factory();
								}
							}
						```
						
				- Usando un ABSTRACT FACTORY. 
						
					- Crear un AbstractFactory y su interfaz.
				
						```cs						
							public class A_AbstractFactory<T> : IA_AbstractFactory<T>
							{
								private readonly Func<T> _factory;
						
								public A_AbstractFactory(Func<T> factory)
								{
									_factory = factory;
								}
						
								public T Create()
								{
									return _factory();
								}
							}
						
							public interface IA_AbstractFactory<T>
							{
								T Create();
							}
						```
						
					- Crear una extensión de IServiceCollection
				
						```cs						
							public static class AbstractFactoryExtension
							{
								public static void AddAbstractFactory<TInterface, TImplementation>(this IServiceCollection services)
								where TInterface : class
								where TImplementation : class, TInterface
								{
									services.AddTransient<TInterface, TImplementation>();
									services.AddSingleton<Func<TInterface>>(x => () => x.GetService<TInterface>()!);
									services.AddSingleton<IA_AbstractFactory<TInterface>, A_AbstractFactory<TInterface>>(); //Registra el factory que llama al Func
								}
							}
						```
						
					- En program.cs inyectamos a través de la extensión AddAbstractFactory.
				
						```cs						
							namespace Patrons
							{
								public class Program
								{
									public static void Main(string[] args)
									{
										....
							
										//builder.Services.AddTransient<ISample_001, Sample_001>();
										//builder.Services.AddSingleton<Func<ISample_001>>(x => () => x.GetService<ISample_001>());
										builder.Services.AddAbstractFactory<ISample_001, Sample_001>();
							
										....
									}
								}
							}
						```
						
					- Modificamos la vista.
				
						```cs						
							@page "/factory"
							
							@inject IA_AbstractFactory<ISample_001> factory
							
							<PageTitle>Factory</PageTitle>
							
							<h1>Factory Pattern</h1>
							
							<h2>@currentTime?.CurrentDateTime</h2>
							
							<button class="btn btn-primary" @onclick="GetNewTime">Get New Time</button>
							
							@code {
								ISample_001? currentTime;
								private void GetNewTime(){
									currentTime = factory.Create();
								}
							}
						```
						
				- Agregar un nuevo ejemplo Usando un ABSTRACT FACTORY. 
						
					- Crear una clase nueva.
				
						```cs						
							namespace Patrons.Samples.Factory
							{
								public interface ISample_002
								{
									int RandomValue { get; set; }
								}
							
								public class Sample_002 : ISample_002
								{
									public int RandomValue { get; set; }
							
									public Sample_002()
									{
										RandomValue = Random.Shared.Next(1, 100);
									}
								}
							}
						```
						
					- En program.cs inyectamos a través de la extensión AddAbstractFactory.
				
						```cs						
							namespace Patrons
							{
								public class Program
								{
									public static void Main(string[] args)
									{
										....
							
										//builder.Services.AddTransient<ISample_001, Sample_001>();
										//builder.Services.AddSingleton<Func<ISample_001>>(x => () => x.GetService<ISample_001>());
										builder.Services.AddAbstractFactory<ISample_001, Sample_001>();
										builder.Services.AddAbstractFactory<ISample_002, Sample_002>();
							
										....
									}
								}
							}
						```
						
					- Modificamos la vista.
				
						```cs						
							@page "/factory"
							
							@inject IA_AbstractFactory<ISample_001> factory
							@inject IA_AbstractFactory<ISample_002> sample2Factory
							
							<PageTitle>Factory</PageTitle>
							
							<h1>Factory Pattern</h1>
							
							<h2>@currentTime?.CurrentDateTime</h2>
							<h2> The random value is: @randomValue?.RandomValue</h2>
							
							<button class="btn btn-primary" @onclick="GetNewTime">Get New Time</button>
							
							@code {
								ISample_001? currentTime;
								ISample_002? randomValue;
								private void GetNewTime(){
									currentTime = factory.Create();
									randomValue = sample2Factory.Create();
								}
							}
						```