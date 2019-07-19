# Auth API

Handle CRUD user and authentication & authorization

# Structure

**Controllers**: Handle controller request, validate and respone

**Helpers**: For handle throw execption, store sercet of jwt, automap data

**Models**: Model database

**Resources**: Define data field of request for validation

**Responses**: Define data field of respone for request

**Presistence**: Database context and initial admin user

**Security**: Hash password, verify password, generate token base on role, auhorize user match ID or Role

**Services**: Handle flow to interact with database context

**Validation**: Create custom validate data field and flow validate of controllers [ValidateModel]

# Document API

For more information see at: \<host>/swagger
