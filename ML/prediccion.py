# Importando las librerías
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd
import pickle
import requests
import json


# Cargando el modelo para comparar resultados
model = pickle.load( open('model.pkl','rb'))
print(model.predict([[3.1]]))