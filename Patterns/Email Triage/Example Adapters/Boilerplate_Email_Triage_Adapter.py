from flask import Flask, jsonify, request

app = Flask(__name__)

# Route for /CategoryPrediction
@app.route('/CategoryPrediction', methods=['POST'])
def getCategoryPredictions():
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
    # payload["AllowedCategories"]
    # payload["EnateInstance"]

    # Perform category prediction logic using the payload
    # Typically this is where the Body and/or Subject are sent to a 3rd
    # party service for classification.
    # The 3rd party service will return a custom/proprietary JSON body which
    # will then need to be converted to schema below.

    # Prepare the response
    response = {
        "PacketGUID": "a08a1a45-6b7c-42b2-8a20-3fa3f3f68b52",
        "CategoryLevel1": "Technology",
        "CategoryLevel2": "Hardware",
        "CategoryLevel3": "Computer Components",
        "PredictionScore": 0.84
    }

    return jsonify(response), 200


# Route for /TestConfiguration
@app.route('/TestConfiguration', methods=['POST'])
def testAdapterConfiguration():
    payload = request.get_json()

    # Perform configuration test logic using the payload

    # Prepare the response
    response = {
        "TestResult": True
    }

    return jsonify(response), 200


if __name__ == '__main__':
    app.run(debug=True)