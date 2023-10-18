from flask import Flask, jsonify, request
app = Flask(__name__)

# Route for /DataExtraction
@app.route('/DataExtraction', methods=['POST'])
def get_data_extracted():
    # Parse request body
    request_body = request.get_json()
    file_details = request_body['FileDetailsForExtraction']

    # Extract data from file
    # TODO: Perform data extraction logic using the provided file details
    # 1) Get Configuration from Marketplace using the Marketplace API https://digitalworker-dev.azure-api.net/api/v3/Adapters/{fileDetails.AdapterId}/ExternalConfigurations/{fileDetails.ConfigurationId} 
    # passing AdapterID and ConfigurationID
    # 2) Get File from IDP Hub to process using hub API endpoint https://digitalworker-dev.azure-api.net/DataExtraction/api/v1/GetFileForExtraction
    # passing DataExtractionCallBackRequest dto object
    # 3) Send file to 3rd party system for extraction - returns JSON
    # This is where you will need to implement some custom logic
    # 4) Store details of Enate File ID and 3rd Party id, json data and details 
    # of any UI url required in db/storage.  this will be required in a callback by 
    # 3rd party and Enate later


    # Create response
    data_extraction_result = {
        'PacketGuid': str(file_details['PacketGuid']),
        'FileGuid': str(file_details['FileGuid']),
        'Source': file_details['Source'],
        'Confidence': 0.9,  # TODO: Replace with actual confidence value
        'JsonResult': '{"key": "value"}',  # TODO: Replace with actual JSON result
        'FileResult': {
            'FileData': 'SGVsbG8gd29ybGQh',  # TODO: Replace with actual file data
            'FileName': 'example.txt'  # TODO: Replace with actual file name
        }
    }

    return jsonify(data_extraction_result), 200


# Route for /DocumentValidationDetails
@app.route('/DocumentValidationDetails', methods=['POST'])
def get_document_validation_details():
    # Parse request body
    request_body = request.get_json()
    validation_details = request_body['DocumentValidationDetailsRequest']

    # Get document validation details
    # Your implementation goes here

    # Create response
    document_validation_details = {
        'FileGuid': str(validation_details['FileGuid']),
        'PacketGuid': str(validation_details['PacketGuid']),
        'FrameHTML': '',  # TODO: Replace with actual frame HTML
        'AccessToken': '',  # TODO: Replace with actual access token
        'FunctionName': ''  # TODO: Replace with actual function name
    }

    return jsonify(document_validation_details), 200


# Route for /ModelList
@app.route('/ModelList', methods=['POST'])
def get_model_list():
    # Parse request body
    request_body = request.get_json()
    model_list_request = request_body['ModelListRequest']

    # Get list of models
    # Your implementation goes here

    # Create response
    model_list = {
        'Models': [
            {
                'ModelName': 'exampleModel1',
                'ModelId': '92958610-0D15-487D-A2AA-7293171BD95E'
            },
            {
                'ModelName': 'exampleModel2',
                'ModelId': 'C75E7F62-9E5C-4F52-BD48-184E3A5A5F88'
            }
        ]
    }

    return jsonify(model_list), 200


# Route for /TestConfiguration
@app.route('/TestConfiguration', methods=['POST'])
def test_adapter_configuration():
    # Parse request body
    request_body = request.get_json()
    configuration_details = request_body['AdapterConfigurationEntryResult']

    # Test adapter configuration
    # Your implementation goes here

    # Create response
    test_configuration_results = {
        'Success': True,  # TODO: Replace with actual success status
        'Messages': [
            {
                'Parameters': ['setting-name'],
                'Type': 'MissingOrIncorrectInformation'
            },
            {
                'Parameters': [],
                'Type': 'ConfigurationEmpty'
            }
        ]
    }

    return jsonify(test_configuration_results), 200


if __name__ == '__main__':
    app.run(debug=True)