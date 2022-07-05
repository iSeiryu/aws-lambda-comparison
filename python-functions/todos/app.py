import json
from unittest import result
import requests
import asyncio

async def do_work(event):
    loop = asyncio.get_running_loop()
    id = event["queryStringParameters"]['id']

    task1 = loop.run_in_executor(None, requests.get, "https://jsonplaceholder.typicode.com/todos/" + id)
    task2 = loop.run_in_executor(None, requests.get, "https://jsonplaceholder.typicode.com/todos/" + id)
    result1, result2 = await asyncio.gather(task1, task2)
    json1 = result1.json()
    json2 = result2.json()
    results = [json1, json2]
    print(results)
    return {
        "statusCode": 200,
        "headers": {
            "Content-Type": "application/json"
        },
        "isBase64Encoded": False,
        "body": json.dumps(results)
    }

def lambda_handler(event, context):
    loop = asyncio.get_event_loop()
    return loop.run_until_complete(do_work(event))
