import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  users : any
  title = 'Dating App'
  constructor(private http: HttpClient){}
  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error : () => {'There has been an error'},
      complete : () =>{console.log('Completed succesfully.')}
    })
  }


}
