from cStringIO import StringIO

from nose.tools import istest, assert_equal

from glimpse.middleware import Middleware

@istest
def wsgi_test_environment_behaves_correctly():
    application = create_application()
    data = output_from_application(application)
    assert_equal('<html></html>', data)

@istest
def middleware_insterts_script_tags_in_returned_data():
    application = create_application()
    middleware = Middleware(application)
    responses = output_from_application(middleware) 
    

def create_application():
    def application(environ, start_response):
        write = start_response('200 OK', [('Content-type', 'text/html')])
        write('<html>')
        return ['</html>'] #TODO

    return application

def output_from_application(application):
    output = StringIO()

    def start_response(status, response_headers, exc_info=None):
        return output.write

    data = application([], start_response)
    
    for item in data:
        output.write(item)

    response = output.getvalue()
    output.close()
    return response
