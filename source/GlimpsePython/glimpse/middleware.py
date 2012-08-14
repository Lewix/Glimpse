class Middleware(object):
    def __init__(self, application):
        self._application = application

    def __call__(self, environ, start_response):
        def start_glimpse_response(status, response_headers, exc_info=None):
            write = start_response(status, response_headers, exc_info)

            def glimpse_write(data):
                write(data + 'script tag')

            return glimpse_write
        
        data = self._application(environ, start_glimpse_response)
        return [item + 'script tag from later iteration' for item in data]

def wrap_application(wsgi_application):
    return Middleware(wsgi_application)
