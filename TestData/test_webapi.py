import socketserver
import http.server
import re

PORT = 9090
API_URL = "api/get_data"
DATA_FILE = "web_test_data.json"

class GetTestData(http.server.SimpleHTTPRequestHandler):
    def do_GET(self):
        if None != re.search(API_URL, self.path):
            self.send_response(200)
            self.send_header('Content-type','application/json')
            self.end_headers()
            with open(DATA_FILE, "rb") as file:
                self.wfile.write(file.read())
            return
        else:
            http.server.SimpleHTTPRequestHandler.do_GET(self)

httpd = socketserver.ThreadingTCPServer(('', PORT), GetTestData)

print("serving at port:", PORT)
httpd.serve_forever()