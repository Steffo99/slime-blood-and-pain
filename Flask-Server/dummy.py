import requests
r = requests.post("http://127.0.0.1:5000/message/2", data={'content':'Piano 2'})
print(r.text)