Pasos para levantar el proyecto
1) Programas que debe de tener:
-> Visual Studio 2022
-> SDK de .net 6
-> SQL Server
2) Configruar el ConnectionStrings 
-> Ir al archivo appsettings.json
-> Cambiar las variables de acuerdo a la conección a su SQLServer


-----------------------------------------------------------------------------------------------
CLean Architecture:
Sirve para desacoplar los casos de uso 
1) Elementos
-> Core:
	* En el Core tenemos dos capas:
		- Application: 
			+ Desarrolla todos los casos de uso de negocios
		- Domain: Capa de donde se definen la estrucura de los datos (Entities)
-> Infrastructure
	* Contiene la implementación de la persistencia declarada en la capa de aplicación; 
	  además contiene un modelo de persistencia (Usamos el EntityFramework CodeFirst) 
	* la implementación de las interfaces declaradas en la capa de application
-> Presentation
	* Corresponde al proyecto Api
	* Creamos los métodos necesarios que el cliente externo va a crear

-----------------------------------------------------------------------------------------------
Patron CQRS (Command Query Responsability Segregation)

-> Permite la comunicación entre capas;
	Ejm: Comunicación entre la capa de Presentation con la capa de Application
-> Separamos los tipos de opración realizada en la Base de datos en dos workflow; los tipo de objetos para nuestra transacción CQRS
	- querys: consultan data de nuestra aplicación y no producen o actualicen ningún valor en la base de datos (Lectura)
			  y retornan datos a la capa de presentación (DTO)
	- commands: Las operaciones de alto nivel que modifican data de nuestra aplicación y que no retornan ningún valor
			  o retona un pequeño valor de data del registro recién registrado

----------------------------------------------------------------------------------------------
Patron Mediator

-> Define un objeto que encapsula la lógica de comunicación entre un conjunto de objeto; es decir  (la definición de las operaciones genericas de comunicación y su administración).
-> Promueve un acoplamiento flexible y evita la referencia explicita entre los objetos (Evitamos hacer una instancia directa de un objeto dentro de otra clase)
-> La interacción de los objetos se da de forma independiente
-> Sirve para comunicar los comandas y queries de sus respectivos handler

-----------------------------------------------------------------------------------------------
Patrón Repository

-> Se utiliza para separar la lógica de negocio de la lógica de acceso a datos en una aplicación.
-> El patrón Repository establece una capa de abstracción entre la aplicación y la fuente de datos (base de datos o un servicio web)  
	Esto permite que la lógica de negocio de la aplicación sea independiente de la forma en que se almacenan y acceden los datos.
-> el patrón Repository proporciona una interfaz para realizar operaciones de lectura y escritura realizadas en una fuente de datos determinada. 
	Esto significa que la lógica de negocio de la aplicación puede trabajar con los datos a través de la interfaz del Repositorio, 
	sin tener que preocuparse por los detalles de cómo se almacenan o acceden los datos.
-------------------------------------------------------------------------------------------------
Patron Builder

