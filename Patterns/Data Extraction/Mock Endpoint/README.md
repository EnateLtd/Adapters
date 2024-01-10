This project includes two variants of the same Mock API Endpoint for the Enate Data Extraction Adapter:
- A mock endpoint without MTLs (which includes a postman collection with example JSON bodies/responses)
- A mock endpoint that <b>can</b> be run with MTLs enabled (instructions below)

<h2>Mock Endpoint With MTLs</h2>

In order to 'stand-up' the endpoint with MTLs you will need to have 'gunicorn' installed and then please run the following from within the MockWithMTLS directory:

gunicorn --bind :5000 --keyfile server.key --certfile server.crt --ca-certs ca-crt.pem --cert-reqs 2 app:app

This will launch the API with MTLs enabled, this can be tested and called with:

curl --insecure --cacert ca-crt.pem --key client.key --cert client.crt https://localhost:5000/

On sucess this should return:
"<\h2>Enate Data Extraction Mock Adapter</\h2>"

<h2>Mock Enpoint Without MTLs</h2>

Please use the provided Postman Collection in order to test and view the data required in order to make REST calls.

The two calls that are the most critical are "GetFileForExtraction" and "DataExtractionCallBack" the former will get the next file from the mock API, responding with the file encoded as Base64, this would then be forwarded via REST to the selected IDP partner (ABBYY/Kofax etc) for analysis, on completion that solution would make a callback to the Adapter which would then call the Endpoint updating Enate with the results <b>and</b> verification endpoint.
