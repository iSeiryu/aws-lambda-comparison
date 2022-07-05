import json
import datetime

def lambda_handler(event, context):
    body = json.loads(event['body'])
    print(body)
    body["title"] = body["title"] + "modified"
    body["title"] = body["title"].upper()
    date = datetime.datetime.strptime(body["published"], "%Y-%m-%d")
    print(date)
    new_date = date + datetime.timedelta(days=1)
    print(new_date)
    body["published"] = new_date.strftime("%Y-%m-%d")
    body["completed"] = not body["completed"]
    print(body)
    return {
        "statusCode": 200,
        "body": json.dumps(body)
    }