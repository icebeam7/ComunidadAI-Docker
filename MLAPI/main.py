from flask import Flask, request
import json
import requests
import os

URL_API_NET = os.environ['URL_API_NET']
#URL_API_NET = 'http://localhost:49657/'

urlApiPrediccion = URL_API_NET + "api/predicciones/"

app = Flask(__name__)

@app.route('/enviar', methods=['POST'])
def enviar():
    datos = request.get_json()

    try:
        tiempo = datos.get('tiempoExperiencia', 0)

        headers = {'Content-type': 'application/json', 'Accept': 'text/plain'}
        data = { 'estatus': 1, 'tiempoExperiencia': tiempo }
        print(data)
        nr = requests.post(urlApiPrediccion, data = json.dumps(data), headers = headers)

        resultado = nr.json() 
        id = resultado['id']

        print ("*** Se recibio una prediccion con ID " + str(id) )
        return {'id': id}, 200, {'ContentType':'application/json'}
    except Exception as e:
        print (str(e))
        return {'id': 0}, 400, {'ContentType':'application/json'}

@app.route("/consultar/<id>")
def consultar(id):
     resultado = {'id': id, 'estatus': 0, 'tiempoExperiencia': 0, 'salario': 0 }     
     urlID = urlApiPrediccion + str(id)
     nr = requests.get(url = urlID)   
     datos = nr.json()
    
     if datos is not None:
        resultado = datos

     return resultado

#if __name__ == "__main__":
#   app.run(debug=True)