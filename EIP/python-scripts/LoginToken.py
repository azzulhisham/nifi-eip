import sys
import base64
import psycopg2     #pip install psycopg2
import datetime
import jwt          #pip install pyjwt


#response object
objPayload = {
    "name": "",
    "email": "",
    "tenant": "",
    "accessToken": "",
    "role": "",
    "expire": "",
    "error": ""
}

#module functions blocks
#access db
def getUserDetail(password):
    resultCount = 0

    try:
        # connect to Postgres DB
        pg_conn = psycopg2.connect(
            host="localhost",
            port=5432,
            database="nifi-eip",
            user="postgres",
            password="123qwe")

        pg_cursor = pg_conn.cursor()

        #query
        query = 'SELECT "Users"."UserName","Users"."EmailAddress","Users"."Password","Tenants"."TenancyName","Roles"."Name" as "UserRole" '
        query += 'FROM public."Users" '
        query += 'JOIN public."Tenants" on "Tenants"."Id" = "Users"."TenantId" '
        query += 'JOIN public."UserRoles" on "UserRoles"."TenantId" = "Tenants"."Id" and "UserRoles"."UserId" = "Users"."Id" '
        query += 'JOIN public."Roles" on "Roles"."Id" = "UserRoles"."Id" '
        query += 'WHERE "Users"."IsDeleted" = false AND "Tenants"."IsDeleted" = false AND "Roles"."IsDeleted" = false '
        query += 'AND "Users"."IsActive" = true AND "Tenants"."IsActive" = true '
        query += 'AND "Users"."EmailAddress" = \'{0}\' AND "Users"."Password" = \'{1}\' AND "Tenants"."TenancyName" = \'{2}\''

        pg_cursor.execute(query.format(objPayload['email'], password, objPayload['tenant']))

        #query result
        resultCount = pg_cursor.rowcount

        if (resultCount > 0):
            now = datetime.datetime.now()
            dt = datetime.timedelta(365)

            for row in pg_cursor:
                objPayload['name'] = row[0]
                objPayload['role'] = row[4]
                objPayload['expire'] = str(now + dt)

        pg_conn.close()
    except:
        resultCount = -1
        pg_conn.close()

    return resultCount



# to get the command line arguments
argv = (sys.argv)

if (len(argv) > 1):
    try:
        objPayload['email'] = argv[1]
        password = argv[2]
        objPayload['tenant'] = argv[3]

        # encode password to base64
        enc = base64.b64encode(bytes(password, 'utf-8'))
        # convert bytes to string
        password = enc.decode('utf-8')
        ret = getUserDetail(password)

        if (ret > 0):
            tokenPayload = {
                "name": "",
                "email": "",
                "tenant": "",
                "role": "",
                "expire": ""
            }

            tokenPayload['name'] = objPayload['name']
            tokenPayload['email'] = objPayload['email']
            tokenPayload['tenant'] = objPayload['tenant']
            tokenPayload['role'] = objPayload['role']
            tokenPayload['expire'] = objPayload['expire']

            jwtToken = jwt.encode(tokenPayload, "pinc.my", algorithm="HS256")
            objPayload['accessToken'] = jwtToken

        elif(ret == 0):
            objPayload['error'] = 'User not found!'
        else:
            objPayload['error'] = 'Database Error!'

    except:
        objPayload['error'] = 'Invalid Request!'

else:
    objPayload['error'] = 'Invalid Request!'


print(objPayload)


