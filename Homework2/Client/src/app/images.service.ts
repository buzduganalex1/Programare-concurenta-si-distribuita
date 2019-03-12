import { Injectable } from '@angular/core';
import { Tag } from './Tag';
import { Image } from './Image';
import { Observable, of} from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {
  url: string;

  constructor(private http: HttpClient) { 
    this.url = "http://localhost:5000";
  }

  getImages(): Observable<Image[]>{
    return this.http.get<Image[]>(this.url + "api/images/")
  }

  getImagesForTag(uri: String): Observable<Image[]>{
    const path = this.url + "api/images/byTag?tagUri=" + uri;
    
    return this.http.get<Image[]>(path)
  }

  getRecommendedPictures(uri: String): Observable<Image[]>{
    const path = this.url + "api/images/similar?imageUri=" + uri;
    
    return this.http.get<Image[]>(path)
  }

  getImageByUri(uri: string){
    const path = this.url + "api/images/byUri?imageUri=" + uri;

    return this.http.get<Image>(path)
  }
}
