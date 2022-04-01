import time
import pickle
import datetime
import requests
import numpy as np
from flask import Flask, request, jsonify, render_template
import os

URL_API_NET = os.environ['URL_API_NET']
#URL_API_NET = 'http://localhost:49657/'
urlApiPrediccion = URL_API_NET + "api/predicciones/"
urlSiguiente = urlApiPrediccion + "siguiente"

model = pickle.load(open('model.pkl', 'rb'))

def predecir():
     try:
        r = requests.get(url = urlSiguiente) 
        datos = r.json() 

        if len(datos) > 0:
           print(datos)
           id = datos[0]['id']
           print ("*** Procesando Prediccion con ID" + str(id) )

           tiempo = datos[0]['tiempoExperiencia']
           prediccion = model.predict([[tiempo]])
           salario = prediccion[0]

           urlID = urlApiPrediccion + str(id)
           headers = {'Content-type': 'application/json', 'Accept': 'text/plain'}
           content = {'id': id, 'estatus': 3, 'tiempoExperiencia': tiempo, 'salario': salario }
           nr = requests.put(url = urlID, json = content, headers = headers) 

           print ("*** Prediccion con ID " + str(id) + " Procesada con codigo " + str(nr.status_code))
     except Exception as e:
           print (str(e))

def print_time():
    current_time = datetime.datetime.now().strftime("%H:%M:%S")
    print("Buscando. Hora local: ", current_time)

def infinite_process():
    while True:
        print_time()
        predecir()
        time.sleep(10)

app = Flask(__name__)

if __name__ == "__main__":
   infinite_process()