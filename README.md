# Deep Dive: Domain-Driven Design

Welcome to the ["Deep Dive: Domain-Driven Design"](https://dometrain.com/course/deep-dive-domain-driven-design-ddd/) course on Dometrain! 

This repository contains the source code for the course, which you can use to follow along.

The **main branch** contains the most up-to-date version of the code, reflecting the latest improvements, updates, and fixes. 

## Running Locally

To run all 3 microservices and test interacting with the system end to end, all you need to do is: 

1. Let docker spin everything up
1. Make requests using the `.http` files

### Let docker spin everything up

Run the following command in the root of the repository 👇

```shell
docker compose up --build
```

### Make requests using the `.http` files

Each one of the 3 microservices has a `requests` folder with the definition of the requests that correspond to the endpoints we have.

For example, the definition of the "Create Gym" request sits under `./GymManagement/requests/Gyms/CreateGym.http` and looks like this

```http
POST {{gym-host}}/subscriptions/{{subscriptionId}}/gyms
Content-Type: application/json

{
    "Name": "Amiko's gym"
}
```

#### ⚠️ Important Note

You'll need to update the variables (in the request above we have `{{gym-host}}` and `{{subscriptionId}}`) with the appropriate values.

If you're using VSCode, make sure to install the extension ["Rest Client"](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) which will allow you to send requests from within VSCode.

When using the Rest Client extension, you can define these variables once, and they will apply throughout all the requests in the repository.

I've included the following variables as part of the source code (available under `./.vscode/settings.json`):

```json
{
    "rest-client.environmentVariables": {
        "$shared": {
            "userId": "a69ad4d4-f139-4248-a380-4dfad6b68ae2",
            "adminId": "7168a078-0e2c-4d5a-a365-4d36b9372050",
            "trainerId": "be7a9faa-5d75-4256-a4c4-3b52bb1e910f",
            "participantId": "460788de-3597-41ea-8092-ecf69afd211d",
            "subscriptionId": "ce159a2f-f264-4ee3-9cff-7e2473d0081c",
            "gymId": "3bffb52a-2a6b-439c-98fb-c17a0e1cb1c8",
            "roomId": "b3641ff2-469d-447c-9ba2-7da52eecba58",
            "roomId2": "07910dea-d044-4a85-af1c-d02dc7591ffc",
            "roomId3": "4c33909d-06c9-46a6-8ffe-d6676c197803",
            "sessionId": "b0b9b8b4-55d6-4d72-abac-830ff223ae1f",
            "sessionId2": "ada99618-965f-41f7-84a1-2b94a2bf445f",
            "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQW1pY2hhaSIsImVtYWlsIjoiYW1pY2hhaUBtYW50aW5iYW5kLmNvbSIsImlkIjoiYTY5YWQ0ZDQtZjEzOS00MjQ4LWEzODAtNGRmYWQ2YjY4YWUyIiwiZXhwIjoxNzM4Njc0MzM5LCJpc3MiOiJVc2VyTWFuYWdlbWVudCIsImF1ZCI6IlVzZXJNYW5hZ2VtZW50In0.YxpRNeqMZSnGlKZ9T1K-ZuassrNE1h8Vt7-ib01WkKk"
        },
        "dev": {
            "host": "http://localhost:5051"
        },
        "docker": {
            "gym-host": "http://localhost:5100",
            "session-host": "http://localhost:5101",
            "user-host": "http://localhost:5102"
        }
    }
}
```

After creating/updating a resource, make sure to update the corresponding variable's value in the settings.

So with that in mind, these are the steps you need to take 👇

1. Set the `Rest Client Environment` to `docker`
    1. To do this, open the command palette (`ctrl` + `shift` + `P`) and search for `Rest Client: Switch Environment`. Then, select `docker`.
1. Create/update resources by executing the various http files (don't forget to update the `./.vscode/settings.json` with the resource's id).
