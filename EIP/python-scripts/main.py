import sys
import jwt
import datetime
import pyodbc
import base64
import psycopg2

#encode string to base64
acode = "123qwe"
enc = base64.b64encode(bytes(acode, 'utf-8'))
print(enc)
#convert bytes to string
str_enc = enc.decode('utf-8')
print(str_enc)
#decode
content = base64.b64decode(str_enc)
#convert bytes to string
content = content.decode('utf-8')
print(content)


# to get the command line arguments
argv = (sys.argv)

if (len(argv) > 1):
    print(argv[1])
    print(argv[2])

# everything about datetime
now = datetime.datetime.now()
strNow = str(now)
#print(strNow)
dd = datetime.datetime.strptime(strNow, "%Y-%m-%d %H:%M:%S.%f")
#print(dd)

dt = datetime.timedelta(1)
#print(dd + dt)


# jwt - json web token
objPayload = {
    "name": "zultan",
    "email": "zultan@pinc.my",
    "expire": str(now)
}

encoded = jwt.encode(objPayload, "pinc.my", algorithm="HS256")
print(encoded)

payload = (jwt.decode(encoded, "pinc.my", algorithms=["HS256"]))
print(payload)
#print(payload['name'])
#print(payload['email'])


# using SQL database
conn = pyodbc.connect(
    "DRIVER={ODBC Driver 17 for SQL Server};"
    "SERVER=DESKTOP-TLVFD7V\SQLEXPRESS;"
    "Database=Marking;"
    "UID=sa;"
    "PWD=Az@HoePinc0615;"
)

cursor = conn.cursor()
#cursor.execute("SELECT * FROM Records WHERE Lot_No='EMDTEST001'")
#for row in cursor:
#    print(row)
#    markDate = row[4]
#    print(markDate + datetime.timedelta(3))

conn.close()


#using Postgres DB
pg_conn = psycopg2.connect(
    host="localhost",
    port=5432,
    database="nifi-eip",
    user="postgres",
    password="123qwe")

pg_cursor = pg_conn.cursor()
pg_cursor.execute('SELECT * FROM public."Users"')
for row in pg_cursor:
    print(row)

pg_conn.close()