import requests

a = requests.post( 'http://127.0.0.1:5000/',{'register':'','name':'gokul','age':'18'})
print(a.text)