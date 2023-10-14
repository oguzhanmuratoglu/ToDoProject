import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { TodoModel } from 'src/models/Todo';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  todos: TodoModel[] = [];
  todo: TodoModel[] = [];
  done: TodoModel[] = [];
  add:boolean = false;
  changeName:boolean=false;
  alert:boolean=false;
  alertMessage: string ="";
  inputValue:string ="";
  request:TodoModel= new TodoModel();
  updateId: number=0;
  updateIsCompleted:boolean = false;
  validate:string="";
  validateDisplay:boolean=false;

  constructor(
    private http: HttpClient
  ){
    this.getAll();
  }
  deleteItem(id:number){
    this.request.id = id;
    this.http.post<TodoModel[]>("https://localhost:7148/api/Todos/DeleteItem", this.request).subscribe(res=> {
      this.todos = res;
      this.alert=true;
      this.validateDisplay=false;
      this.alertMessage = "Item Deleted Successfully"
      this.getAll();
    });
  }
  UpdateToDatabase(){
    if(this.inputValue===""){
      this.validateDisplay=true;
      this.validate="Input Value Cannot Be Empty";
      return;
    }
    this.request.work = this.inputValue;
    this.request.isCompleted = this.updateIsCompleted;
    this.request.id = this.updateId;
    this.http.post<TodoModel[]>("https://localhost:7148/api/Todos/EditItem", this.request).subscribe(res=> {
      this.todos = res;
      this.changeName=false;
      this.alert=true;
      this.validateDisplay=false;
      this.alertMessage = "Item Updated Successfully"
      this.getAll();
    });
  }
  addToDatabase(){
    if(this.inputValue===""){
      this.validateDisplay=true;
      this.validate="Input Value Cannot Be Empty";
      return;
    }
    this.request.work = this.inputValue;
    this.http.post<TodoModel[]>("https://localhost:7148/api/Todos/AddItem", this.request).subscribe(res=> {
      this.todos = res;
      this.add=false;
      this.alert=true;
      this.validateDisplay=false;
      this.alertMessage = "Item Added Successfully"
      this.getAll();
    });
  }
  changeItem(item:TodoModel){
    this.updateId = item.id;
    this.updateIsCompleted = item.isCompleted;
    this.inputValue = item.work;

    this.changeName=true;
    this.alert=false;
    this.add=false;
  }
  cancel(){
    this.add=false;
    this.changeName=false;
    this.validateDisplay=false;
  }

  addItem(){
    this.add = true;
    this.alert=false;
    this.changeName=false;
    this.validateDisplay=false;
    this.inputValue="";
  }

  getAll(){
    this.http.get<TodoModel[]>("https://localhost:7148/api/Todos/GetAll")
    .subscribe(res=>{
      this.todos = res;
      this.splitTodosToTodoAndDone();
    })
  }

  splitTodosToTodoAndDone(){
    this.todo = [];
    this.done = [];
    for(let t of this.todos){
      if(t.isCompleted) this.done.push(t);
      else this.todo.push(t);
    }
  }

  changeCompleted(id: number){
    this.http.get<TodoModel[]>(`https://localhost:7148/api/Todos/ChangeCompleted/${id}`)
    .subscribe(res=>{
      this.todos = res;
      this.splitTodosToTodoAndDone();
    })
  }

  drop1(event: CdkDragDrop<TodoModel[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      const id = this.done[event.previousIndex].id;      
      this.changeCompleted(id);
    }
  }  

  drop2(event: CdkDragDrop<TodoModel[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      const id = this.todo[event.previousIndex].id;
      this.changeCompleted(id);
    }
  }  
}

