import { APIGatewayProxyEvent, APIGatewayProxyResult } from 'aws-lambda';
import axios, { AxiosResponse } from 'axios';

/**
 *
 * Event doc: https://docs.aws.amazon.com/apigateway/latest/developerguide/set-up-lambda-proxy-integrations.html#api-gateway-simple-proxy-for-lambda-input-format
 * @param {Object} event - API Gateway Lambda Proxy Input Format
 *
 * Return doc: https://docs.aws.amazon.com/apigateway/latest/developerguide/set-up-lambda-proxy-integrations.html
 * @returns {Object} object - API Gateway Lambda Proxy Output Format
 *
 */

const logStart = (): void => {
    console.info("[Information] Microsoft.Hosting.Lifetime: Application started. Press Ctrl+C to shut down. ")
    console.info("[Information] Microsoft.Hosting.Lifetime: Hosting environment: Production ")
    console.info("[Information] Microsoft.Hosting.Lifetime: Content root path: /var/task ")
    console.info("059b8e39-48b3-4875-9b30-ea039910e1d4	[Information] Microsoft.AspNetCore.Hosting.Diagnostics: Request starting  GET https://nm7alnytm1.execute-api.us-east-1.amazonaws.com/Prod/ - 0 ")
    console.info("059b8e39-48b3-4875-9b30-ea039910e1d4	[Information] Microsoft.AspNetCore.Routing.EndpointMiddleware: Executing endpoint '/ HTTP: GET' ")
    console.info("059b8e39-48b3-4875-9b30-ea039910e1d4	[Information] Microsoft.AspNetCore.Routing.EndpointMiddleware: Executed endpoint '/ HTTP: GET' ")
    console.info("059b8e39-48b3-4875-9b30-ea039910e1d4	[Information] Microsoft.AspNetCore.Hosting.Diagnostics: Request finished  GET https://nm7alnytm1.execute-api.us-east-1.amazonaws.com/Prod/ - 0 - 200 - - 221.0171ms")
}

export const lambdaHandler = async (event: APIGatewayProxyEvent): Promise<APIGatewayProxyResult> => {
    logStart()
    const id = (event.queryStringParameters ?? {})["id"]
    if(id === undefined) {
        return {
            statusCode: 400,
            body: JSON.stringify({
                message: "No id found"
            })
        }
    }
    const results = await Promise.all([axios.get(`https://jsonplaceholder.typicode.com/todos/${id}`), axios.get(`https://jsonplaceholder.typicode.com/todos/${id}`)])
    const mappedResults = results.map((response) => response.data)
    return {
        statusCode: 200,
        body: JSON.stringify(mappedResults)
    }
};
