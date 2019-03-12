import { Injectable } from '@angular/core';
import { Tag } from './Tag';
import { Image } from './Image';
import { Observable, of} from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TagsService {
  url: string;

  constructor(private http: HttpClient) { 
    this.url = "http://localhost:5000";
  }

  public getTags(): Observable<Tag[]>{
    return this.http.get<Tag[]>(this.url + "api/tags/all")
  }

  public getTagsForImage(uri: string): Observable<Tag[]>{
    const path = this.url + "api/tags/forImageUri?imageUri=" + uri;

    return this.http.get<Tag[]>(path)
  }

  public getTag(uri: string): Observable<Tag>{
    const path = this.url + "api/tags/byUri?tagUri=" + uri;

    return this.http.get<Tag>(path);
  }
}
