import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JwtinterceptorService implements HttpInterceptor {

  constructor() { }
// Define an intercept method that takes in an HttpRequest object and a HttpHandler object and returns an Observable of type HttpEvent.
intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  // Create a variable currentUser and initialize its token property to an empty string.
  var currentUser={token:""};
  // Retrieve the currentUserSession value from the sessionStorage using the key "currentUser".
  var currentUserSession=sessionStorage.getItem("currentUser");
  // Check if the currentUserSession value is not null.
  if(currentUserSession !=null){
    // If the currentUserSession value is not null, parse it into a JSON object and assign it to the currentUser variable.
    currentUser=JSON.parse(currentUserSession);
  }
  // Create a clone of the HttpRequest object and set the headers to include an Authorization header with a value of "Bearer" followed by the token value from the currentUser object.
  req =req.clone({
    setHeaders:{
      Authorization: 'Bearer ' + currentUser.token
    }
  })
  // Return the next HttpHandler object's handle() method with the modified HttpRequest object.
  return next.handle(req);
}


}

