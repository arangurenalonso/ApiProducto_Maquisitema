Pasos para levantar el proyecto
1) Programas que debe de tener:
-> Visual Studio 2022
-> SDK de .net 6
-> SQL Server
2) Configruar el ConnectionStrings 
-> Ir al archivo appsettings.json
-> Cambiar las variables de acuerdo a la conecci�n a su SQLServer


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
	* Contiene la implementaci�n de la persistencia declarada en la capa de aplicaci�n; 
	  adem�s contiene un modelo de persistencia (Usamos el EntityFramework CodeFirst) 
	* la implementaci�n de las interfaces declaradas en la capa de application
-> Presentation
	* Corresponde al proyecto Api
	* Creamos los m�todos necesarios que el cliente externo va a crear

-----------------------------------------------------------------------------------------------
Patron CQRS (Command Query Responsability Segregation)

-> Permite la comunicaci�n entre capas;
	Ejm: Comunicaci�n entre la capa de Presentation con la capa de Application
-> Separamos los tipos de opraci�n realizada en la Base de datos en dos workflow; los tipo de objetos para nuestra transacci�n CQRS
	- querys: consultan data de nuestra aplicaci�n y no producen o actualicen ning�n valor en la base de datos (Lectura)
			  y retornan datos a la capa de presentaci�n (DTO)
	- commands: Las operaciones de alto nivel que modifican data de nuestra aplicaci�n y que no retornan ning�n valor
			  o retona un peque�o valor de data del registro reci�n registrado

----------------------------------------------------------------------------------------------
Patron Mediator

-> Define un objeto que encapsula la l�gica de comunicaci�n entre un conjunto de objeto; es decir  (la definici�n de las operaciones genericas de comunicaci�n y su administraci�n).
-> Promueve un acoplamiento flexible y evita la referencia explicita entre los objetos (Evitamos hacer una instancia directa de un objeto dentro de otra clase)
-> La interacci�n de los objetos se da de forma independiente
-> Sirve para comunicar los comandas y queries de sus respectivos handler

-----------------------------------------------------------------------------------------------
Patr�n Repository

-> Se utiliza para separar la l�gica de negocio de la l�gica de acceso a datos en una aplicaci�n.
-> El patr�n Repository establece una capa de abstracci�n entre la aplicaci�n y la fuente de datos (base de datos o un servicio web)  
	Esto permite que la l�gica de negocio de la aplicaci�n sea independiente de la forma en que se almacenan y acceden los datos.
-> el patr�n Repository proporciona una interfaz para realizar operaciones de lectura y escritura realizadas en una fuente de datos determinada. 
	Esto significa que la l�gica de negocio de la aplicaci�n puede trabajar con los datos a trav�s de la interfaz del Repositorio, 
	sin tener que preocuparse por los detalles de c�mo se almacenan o acceden los datos.
-------------------------------------------------------------------------------------------------
Patron Unit of Work

El patrón Unit of Work (unidad de trabajo) 
-> se utiliza para administrar las transacciones y la persistencia de los datos en una aplicación. 

-> Rastrea los cambios realizados en los objetos del modelo de datos y coordinar la escritura de estos cambios en la base de datos
	(Como un coordinador de operaciones de escritura). 
	- Principalmente se utiliza en aplicaciones de gran escala que necesitan realizar muchas operaciones de escritura en la base de datos. 
	- todas las operaciones de escritura se realicen de manera coherente y que se puedan deshacer en caso de que ocurra algún error o excepción
	- También puede mejorar el rendimiento de la aplicación, ya que puede agrupar múltiples operaciones de escritura en una única transacción de base de datos.
	- Asi ya no nos preocupamos por las transacciones y la persistencia de los datos