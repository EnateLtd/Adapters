from flask import Flask, request, jsonify

app = Flask(__name__)

# Route for /TestConfiguration
@app.route('/TestConfiguration', methods=['POST'])
def test_adapter_configuration():
    payload = request.get_json()

    # Perform validation logic here    

    return jsonify(True)

# Route for /EntityExtraction
@app.route('/EntityExtraction', methods=['POST'])
def entity_extraction():
    email_details = request.get_json()
            
    # available payload attributes
    # payload["AdapterId"]
    # payload["ConfigurationId"]
    # payload["Guid"]
    # payload["PacketGUID"]
    # payload["Subject"]
    # payload["Body"]
    # payload["Categories"]
    # payload["DataFields"]
    # payload["EnateInstance"]
        
    # Perform entity extraction logic here
    # This is where the DataFields and Body/Subject are passed to the entity extraction service
    # The data fields indicate the data that we wish to extract.
    # The service will return a custom/proprietary schema which will then
    # need to be converted to the schema below.
    
    extraction_result = {
        "PacketGuid": email_details["PacketGuid"],
        "ExtractionResult": {
            "example_key": {
                "example_property": "example_value"
            }
        }
    }
    
    return jsonify(extraction_result)

if __name__ == '__main__':
    app.run(debug=True)