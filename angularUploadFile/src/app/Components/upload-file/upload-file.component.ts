import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http';


@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {

  public message :string = "";
  public progress :number = 0;

  @Output() public onUploadFinish = new EventEmitter();


  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  uploadFile :any = (files:any) => {
      if(files.length === 0)
        return;

      let fileToUpload = <File>files[0];
      const formData = new FormData();
      formData.append('file' , fileToUpload , fileToUpload.name);


      this.http.post('https://localhost:7289/api/Uploadfile' , formData , {reportProgress : true , observe : 'events'})
      .subscribe(event => {
        if(event.type === HttpEventType.UploadProgress){
           this.progress = Math.round(100 * event.loaded / (event.total?? 1));
        }
        else if(event.type === HttpEventType.Response){
            this.message = 'upload success';
            this.onUploadFinish.emit(event.body);
        }
      })
  }

}
