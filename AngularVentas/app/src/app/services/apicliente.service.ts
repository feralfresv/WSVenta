import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiclienteService {

  url: string = 'http://localhost:5000/api/cliente';
  constructor(
    private _http: HttpClient
  ) { }

  getClientes(): Observable<Response>{
      return this._http.get<Response>(this.url);
  }
}
