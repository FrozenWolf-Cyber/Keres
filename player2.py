import requests

# print(requests.post('http://127.0.0.1:5000/', {"match_making":'cJVrpLH389mGv&8&3&10&0'}).text)
print(requests.post('http://127.0.0.1:5000/', {"check_for_match":'cJVrpLH389mGv'}).text)