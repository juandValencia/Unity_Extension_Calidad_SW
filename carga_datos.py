# -*- coding: utf-8 -*-

import pymysql
import pandas as pd
from sqlalchemy import create_engine

file_name = 'Personajes_pantheon.csv'

df = pd.read_csv(file_name)

print('Se cargo el archivo ',file_name)
print(df)

print('conectandose a MYSQL')

mysql_con = '34.74.9.161'
conection = create_engine('mysql+pymysql://root@34.74.9.161:3306/gestion')

print('Cargando datos...')

df.to_sql(name='personajes_test', con=conection, if_exists='replace')

print('Datos cargados a MYSQL')
