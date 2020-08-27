import requests
import time

a = time.time()

print(requests.post('https://thekeres.herokuapp.com/', {"master_control":'SELECT * FROM PLAYER_INFO '}).text)

b = time.time()

print(b-a)
# print(requests.post('http://127.0.0.1:5000/', {"login":'dummy&rlstine', "type_of_login":"username"}).text)
# print(requests.post('https://thekeres.herokuapp.com/', {"master_control":'DESC PLAYER_INFO'}).text)