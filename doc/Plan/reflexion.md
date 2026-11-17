# Evaluación

# Reflexión: Agregar jugadores a la platilla del usuario

Ahora que he creado el programa, entendí que estaba en lo correcto por que el id además del id de la plantilla , me sirve para verificar en la lógica de negocio que el futbolista si existe y  además me ayuda a ver si su posición aún tiene espacio en las plantilla, gracias  a los métodos de IRepoPosición que utilizó para saber qué posición tiene el futbolista.

# Reflexión: el usuario quiere ver la puntuación del jugador en una fecha específica

Ahora que he creado el programa y vi la hipótesis, entendí que estaba en lo correcto porque con el id puede identificar el jugador y con la fecha  logró saber si el jugador jugó en una fecha.


# funcionamiento de cada capa

Modelo: La capa de Modelo funciona con un molde  que es comparado con los datos entrantes.

repositorio: esta capa funciona como un mensaje entre el programa y la base de datos , que manda consultas o le dice que agregue algo a la base de datos o lo elimine o actualice. 

Service: esta capa es la lógica de negocio del sistema que se encarga de validar que los datos entrantes sigan los criterios establecidos.

controladores : sirven para recibir, interpretar y responder a las solicitudes HTTP que llegan a una aplicación o servidor web. Actúan como el puente entre las peticiones de los clientes (como un navegador o una app móvil) y la lógica interna o los datos del programa. 

