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