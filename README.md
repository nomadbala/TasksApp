# Service for compiling task lists

This project is service for compiling task lists build with C#. It provides a simple todo list.

### Installation
1. Clone the repository
```
git clone https://github.com/nomadbala/TasksApp.git
```
2. Move to folder
```
cd TasksApp
```
3. Build application using Makefile
```
make build
```
4. Run application using Makefile
```
make up
```
5. Visit `http://localhost:8080/`

### API Documentation
The API documentation can be found at `http://localhost:8080/swagger/index.html` once the application is running. It provides detailed information about the available endpoints and how to use them

### Endpoints
#### Healthcheck
```http
GET /health
```

#### Making a new task
```http
POST /api/todo-list/tasks
```

#### Update existing task
```http
PUT /api/todo-list/tasks/{ID}
```

#### Delete existing task
```http
DELETE /api/todo-list/tasks/{ID}
```

#### Mark task done
```http
PUT /api/todo-list/tasks/{ID}/done
```

### Get tasks by status
```http
GET /api/todo-list/tasks?status=active или GET /api/todo-list/tasks?status=done
```

## You can also access project on 
`https://tasksapp-rdsr.onrender.com/`

## License
You can check it in LICENSE file
