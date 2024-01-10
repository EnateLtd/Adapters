from flask import Flask, jsonify, request

app = Flask(__name__)

# Route for /TestConfiguration
@app.route('/TestConfiguration', methods=['POST'])
def test_configuration():
    try:
        payload = request.get_json()
        # Perform the configuration test here
        
        # Return a successful response
        return jsonify(True), 200
    except:
        # Return an error response if the test fails or an exception occurs
        return jsonify(message="Test failed"), 500

# Route for /IsThankYou
@app.route('/IsThankYou', methods=['POST'])
def is_thank_you():
    try:
        payload = request.get_json()
        # available payload attributes
        # payload["AdapterId"]
        # payload["ConfigurationId"]
        # payload["Guid"]
        # payload["PacketGUID"]
        # payload["From"]
        # payload["To"]
        # payload["Subject"]
        # payload["Body"]
        # payload["SentOn"]
        # payload["ProcessedOn"]
        # payload["OriginalStatus"]
        # payload["EnateInstance"]

        # Perform the is thank you prediction here
        # Typically this is where the Body and/or Subject are sent to a 3rd
        # party service for classification.
        # The 3rd party service will return a custom/proprietary JSON body which
        # will then need to be converted to schema below.

        # Return the prediction result
        result = {
            "Guid": payload['Guid'],
            "ProcessedOn": payload['ProcessedOn'],
            "PredictionScore": 0.76
        }
        return jsonify(result), 200
    except:
        # Return an error response if the prediction fails or an exception occurs
        return jsonify(message="Prediction failed"), 500

if __name__ == '__main__':
    app.run()