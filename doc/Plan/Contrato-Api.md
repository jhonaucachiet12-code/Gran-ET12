# Actividad 2: Contrato de la API

# 1.
GET /api/equipos → 200, 
se espera: una lista de equipos y el codigo 200.

Datos de entrada: consulta sql

Datos de salida: 
[
  {
    "idEquipo": 2,
    "nombre": "Barcelona"
  },
  {
    "idEquipo": 1,
    "nombre": "Boca Juniors"
  },
  {
    "idEquipo": 3,
    "nombre": "Real Madrid"
  }
]

Codigo http de salida: 200

# 2.

GET /api/futbolistas → 200
se espera: lista de futbolistas con equipo y cotización.

Datos de entrada: consulta sql


Datos de salida: 

[
  {
    "idJugador": 1,
    "idPosicion": 4,
    "idEquipo": 1,
    "nombre": "Karim",
    "apellido": "Benzema",
    "apodo": "KB9",
    "nacimiento": "1987-12-19T00:00:00",
    "cotización": 50.00,
    "posicion": {
      "idPosicion": 4,
      "nombre": "Delantero"
    },
    "equipo": {
      "idEquipo": 1,
      "nombre": "Boca Juniors"
    }
  },
  {
    "idJugador": 2,
    "idPosicion": 4,
    "idEquipo": 2,
    "nombre": "Robert",
    "apellido": "Lewandowski",
    "apodo": "Lewy",
    "nacimiento": "1988-08-21T00:00:00",
    "cotización": 60.00,
    "posicion": {
      "idPosicion": 4,
      "nombre": "Delantero"
    },
    "equipo": {
      "idEquipo": 2,
      "nombre": "Barcelona"
    }
  },
  {
    "idJugador": 3,
    "idPosicion": 3,
    "idEquipo": 1,
    "nombre": "Luka",
    "apellido": "Modric",
    "apodo": "Lukita",
    "nacimiento": "1985-09-09T00:00:00",
    "cotización": 40.00,
    "posicion": {
      "idPosicion": 3,
      "nombre": "Mediocampista"
    },
    "equipo": {
      "idEquipo": 1,
      "nombre": "Boca Juniors"
    }
  }
]

Codigo http de salida: 200


# 3.

POST /api/futbolistas → 201, 
se espera: futbolista creado. sin erores;

Datos de entrada: 
{
  "idPosicion": 4,
  "idEquipo": 1,
  "nombre": "Alan",
  "apellido": "Godoy",
  "apodo": "cuca",
  "nacimiento": "1990-01-01T00:00:00",
  "cotización": 100000
}

Datos de salida: 
{
  "idJugador": 4,
  "idPosicion": 4,
  "idEquipo": 1,
  "nombre": "Alan",
  "apellido": "Godoy",
  "apodo": "cuca",
  "nacimiento": "1990-01-01T00:00:00",
  "cotización": 100000,
  "posicion": null,
  "equipo": null
}

Codigo http de salida: 201




POST /api/futbolistas → 201, 
se espera: el error 400 de http. significa que se ingreso un dato invalido;

Datos de entrada: 
{
  "idPosicion": 4,
  "idEquipo": 1,
  "nombre": "",
  "apellido": "Godoy",
  "apodo": "cuca",
  "nacimiento": "1990-01-01T00:00:00",
  "cotización": 100000
}

Datos de salida: 
El nombre del jugador es obligatorio. (Parameter 'jugador')

Codigo http de salida: 400


POST /api/futbolistas → 201, 
se espera: el codigo de error 409 de http. significa que los datos estan duplicados ;

Datos de entrada: 
{
  "idPosicion": 4,
  "idEquipo": 1,
  "nombre": "Alan",
  "apellido": "Godoy",
  "apodo": "cuca",
  "nacimiento": "1990-01-01T00:00:00",
  "cotización": 100000
}

Datos de salida: 
no se puede agregar un jugador que ya existe 

Codigo http de salida: 409

NOTA:Este escenario no lo tenía contemplado debido a que asumí aunque sea improbable, que era posible que existiera un jugador con los mismos datos. excepto el id que es auto incremental.


# 4.

PUT /api/futbolistas/{id} → 200, 
se espera: futbolista con equipo y cotización actualizados

Datos de entrada: 
{
  "idJugador": 4,
  "idPosicion": 4,
  "idEquipo": 2,
  "nombre": "Alan",
  "apellido": "Godoy",
  "apodo": "cuca",
  "nacimiento": "1990-01-01T00:00:00",
  "cotización": 1000000.00,
  "posicion": {
    "idPosicion": 4,
    "nombre": "Delantero"
  },
  "equipo": {
    "idEquipo": 1,
    "nombre": "Boca Juniors"
  }
}

Datos de salida: 

{
  "idJugador": 4,
  "idPosicion": 4,
  "idEquipo": 2,
  "nombre": "Alan",
  "apellido": "Godoy",
  "apodo": "cuca",
  "nacimiento": "1990-01-01T00:00:00",
  "cotización": 1000000.00,
  "posicion": {
    "idPosicion": 4,
    "nombre": "Delantero"
  },
  "equipo": {
    "idEquipo": 2,
    "nombre": "Barcelona"
  }
}

NOTA: en realidad no muestra datos. los datos de salida lo consegui con el get jugador que pide el ID.

Codigo http de salida: 204



se espera: el error 400 de http

Datos de entrada: 
{
  "idJugador": 4,
  "idPosicion": 4,
  "idEquipo": 2,
  "nombre": "",
  "apellido": "Godoy",
  "apodo": "cuca",
  "nacimiento": "1990-01-01T00:00:00",
  "cotización": 1000000.00,
  "posicion": {
    "idPosicion": 4,
    "nombre": "Delantero"
  },
  "equipo": {
    "idEquipo": 2,
    "nombre": "Barcelona"
  }
}


Datos de salida:
El nombre del jugador es obligatorio. (Parameter 'jugador')


Codigo http de salida: 400




se espera: el codigo de error 404

Datos de entrada: 
Id = 9999
{
    "idPosicion": 4,
    "idEquipo": 1,
    "nombre": "Karim",
    "apellido": "Benzema",
    "apodo": "KB9",
    "nacimiento": "1987-12-19T00:00:00",
    "cotización": 50.00
}

Datos de salida: 

{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Not Found",
  "status": 404,
  "traceId": "00-182ac791903b681ac4a705239fd12378-b25c4e316f807bf1-00"
}

Codigo http de salida: 404

NOTA: DTO para actualizar jugador 

# 5.

POST /api/usuarios → 201,

se espera: usuario creado sin exponer su contraseña;

Datos de entrada: 
{
  "nombre": "david",
  "apellido": "jira",
  "email": "ff@d.22",
  "fechaNacimiento": "1990-01-01T00:00:00",
  "idRol": 1,
  "passwordHash": "123456789"
}

Datos de salida: 
{
  "idUsuario": 45,
  "nombre": "david",
  "apellido": "jira",
  "email": "ff@d.22",
  "fechaNacimiento": "1990-01-01T00:00:00",
  "passwordHash": "$2a$11$yLCZ9NP2lVKg65iS932Yi.EE0NJ1/IMvEj2pwzKjdNdCeFj.c7t/.",
  "idRol": 1,
  "roles": {
    "idRol": 0,
    "nombre": ""
  }
}

Codigo http de salida: 201



se espera: el codigo 400

Datos de entrada: 

{
  "nombre": "",
  "apellido": "jira",
  "email": "ff@d.224",
  "fechaNacimiento": "1990-01-01T00:00:00",
  "idRol": 1,
  "passwordHash": "123456789"
}

Datos de salida: 
El nombre del usuario es obligatorio. (Parameter 'usuario')

Codigo http de salida: 400



se espera: el codigo 409


Datos de entrada: 
{
  "nombre": "david",
  "apellido": "jira",
  "email": "ff@d.22",
  "fechaNacimiento": "1990-01-01T00:00:00",
  "idRol": 1,
  "passwordHash": "123456789"
}

Datos de salida:

El email ya está en uso.

Codigo http de salida: 409

# 6.

GET /api/equipos → 200, 
se espera: 

Datos de entrada: 

Datos de salida: 


Codigo http de salida: 

# 7.

GET /api/equipos → 200, 
se espera: 

Datos de entrada: 

Datos de salida: 


Codigo http de salida: 

# 8.

GET /api/equipos → 200, 
se espera: 

Datos de entrada: 

Datos de salida: 


Codigo http de salida: 

# 9.

GET /api/equipos → 200, 
se espera: 

Datos de entrada: 

Datos de salida: 


Codigo http de salida: 

# 10.

GET /api/equipos → 200, 
se espera: 

Datos de entrada: 

Datos de salida: 


Codigo http de salida: 

# 11.

GET /api/equipos → 200, 
se espera: 

Datos de entrada: 

Datos de salida: 


Codigo http de salida: 



# nota: preguntar si los errores diferentes a el 400 se tiene que poner en el controlador o el service , como el 404 o 409

