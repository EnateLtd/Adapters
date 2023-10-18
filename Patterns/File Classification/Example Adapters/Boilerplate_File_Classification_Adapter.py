from flask import Flask, request, jsonify

app = Flask(__name__)

# Route for /FileClassification
@app.route('/FileClassification', methods=['POST'])
def get_file_classification():
    payload = request.get_json()
    
    # available payload attributes
    # payload["AdapterId"]
    # payload["ConfigurationId"]    
    # payload["PacketGUID"]
    # payload["FileGUID"]
    # payload["ModelId"]
    # payload["Source"]
    # payload["EnateInstance"]

    # Perform file classification logic here
    
    file_classification_result = {
        "PacketGuid": file_details["PacketGuid"],
        "FileGuid": file_details["FileGuid"],
        "Tags": {
            "Name": "Pay Slip",
            "Confidence": 0.87
        }
    }
    
    return jsonify(file_classification_result), 200

# Route for /ModelList
@app.route('/ModelList', methods=['POST'])
def get_model_list():
    model_list_request = request.get_json()
    
    # Perform model list logic here
    
    model_list_result = {
        "Models": [
            {
                "ModelName": "exampleModel1",
                "ModelId": "92958610-0D15-487D-A2AA-7293171BD95E"
            },
            {
                "ModelName": "exampleModel2",
                "ModelId": "C75E7F62-9E5C-4F52-BD48-184E3A5A5F88"
            }
        ]
    }
    
    return jsonify(model_list_result), 200

# Route for /TestConfiguration
@app.route('/TestConfiguration', methods=['POST'])
def test_adapter_configuration():
    test_configuration_details = request.get_json()
    
    # Perform test configuration logic here
    
    test_configuration_results = {
        "Success": True,
        "Messages": [
            {
                "Parameters": [ "setting-name" ],
                "Type": "MissingOrIncorrectInformation"
            },
            {
                "Parameters": [],
                "Type": "ConfigurationEmpty"
            }
        ]
    }
    
    return jsonify(test_configuration_results), 200

if __name__ == '__main__':
    app.run(debug=True)