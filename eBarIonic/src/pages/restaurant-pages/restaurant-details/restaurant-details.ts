import { Component,ViewChild, ElementRef, Input } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController, Button } from 'ionic-angular';
import { UserServiceProvider } from '../../../providers/user-service/user-service';

class CountiesAndCities{
  id:number;
  County:string;
  City:string
}

@IonicPage()
@Component({
  selector: 'page-restaurant-details',
  templateUrl: 'restaurant-details.html',
  providers: [UserServiceProvider]
})
export class RestaurantDetailsPage {
  userLogged:boolean = false;
  loggedUser:any;
  restaurant:Object = new Object();
  viewMode:boolean = true;
  restaurantTypes:Array<Object> = [];
  counties:Array<Object> = [];
  county: any;
  city:any;
  file:any;

  @ViewChild('filePreview') filePreview: ElementRef;
  @ViewChild('clearImageButton') clearImageButton: Button;
  @ViewChild('searchImageButton') searchImageButton: Button;
  @ViewChild('fileInput') fileInput: ElementRef;

  constructor(public navCtrl: NavController, public navParams: NavParams, private alertCtrl : AlertController, private userService:UserServiceProvider) {
    this.userLogged = this.userService.isLoggedIn;
    var pageMode =  this.navParams.get('pageMode');
     if(pageMode != null && pageMode == 'Edit')
     {
        this.viewMode = false;
        //get data for rest types... 

     }
     else{
      this.getRestaurantDetails();
    }
    this.getCounties();
    this.getCitiesByCounty();
}

  ionViewDidLoad() {
    console.log('ionViewDidLoad RestaurantDetailsPage');
    this.userService.getUser().then(user => {
      if (user == null) {
        this.showError("Acces neautorizat. Pentru a accesa pagina dorita te rugam sa te autentifici mai intai!");
      }
      else{
        this.loggedUser = user;
        this.createRestaurantTypesArray();
      }
    });
  }

  editRestaurant(){
    this.navCtrl.push("RestaurantDetailsPage", {pageMode: 'Edit'})
  }

  removeFile(){
    this.file = null;
    this.filePreview.nativeElement.value = "";
    this.clearImageButton._elementRef.nativeElement.style.display = 'none';
    this.searchImageButton._elementRef.nativeElement.style.display = 'inline-block';
    this.fileInput.nativeElement.value = "";
  }

  fileUpload(e){
    if(e.currentTarget.files.length > 0)
    {
      this.file = e.currentTarget.files[0];
      this.filePreview.nativeElement.value = this.file.name;
      this.clearImageButton._elementRef.nativeElement.style.display = 'inline-block';
      this.searchImageButton._elementRef.nativeElement.style.display = 'none';
    }
  }

  getCounties(){
    this.counties.push({id:"1",	value:"Alba"});
    this.counties.push({id:"79", value:"Arad"});
    this.counties.push({id:"157", value:"Argeș"});
    this.counties.push({id:"259", value:"Bacău"});
    this.counties.push({id:"352", value:"Bihor"});
    this.counties.push({id:"453", value:"Bistrița-Năsăud"});
    this.counties.push({id:"515", value:"Botoșani"});
    this.counties.push({id:"593", value:"Brașov"});
    this.counties.push({id:"651", value:"Brăila"});
    this.counties.push({id:"695", value:"Buzău"});
    this.counties.push({id:"782", value:"Caraș-Severin"});
    this.counties.push({id:"850", value:"Călărași"});
    this.counties.push({id:"914", value:"Cluj"});
    this.counties.push({id:"995", value:"Constanța"});
    this.counties.push({id:"1065", value:"Covasna"});
    this.counties.push({id:"1110", value:"Dâmbovița"});
    this.counties.push({id:"1199", value:"Dolj"});
    this.counties.push({id:"1310", value:"Galați"});
    this.counties.push({id:"1375", value:"Giurgiu"});
    this.counties.push({id:"1429", value:"Gorj"});
    this.counties.push({id:"1499", value:"Harghita"});
    this.counties.push({id:"1566", value:"Hunedoara"});
    this.counties.push({id:"1635", value:"Iași"});
    this.counties.push({id:"1733", value:"Ialomița"});
    this.counties.push({id:"1799", value:"Ilfov"});
    this.counties.push({id:"1839", value:"Maramureș"});
    this.counties.push({id:"1915", value:"Mehedinți"});
    this.counties.push({id:"1981", value:"Mureș"});
    this.counties.push({id:"2083", value:"Neamț"});
    this.counties.push({id:"2166", value:"Olt"});
    this.counties.push({id:"2278", value:"Prahova"});
    this.counties.push({id:"2382", value:"Satu Mare"});
    this.counties.push({id:"2447", value:"Sălaj"});
    this.counties.push({id:"2508", value:"Sibiu"});
    this.counties.push({id:"2572", value:"Suceava"});
    this.counties.push({id:"2686", value:"Teleorman"});
    this.counties.push({id:"2783", value:"Timiș"});
    this.counties.push({id:"2882", value:"Tulcea"});
    this.counties.push({id:"2933", value:"Vâlcea"});
    this.counties.push({id:"3022", value:"Vaslui"});
    this.counties.push({id:"3108", value:"Vrancea"});
    }

  getCitiesByCounty(){
    if(this.county != null)
    {
      //get cities by county id
      console.log(this.county.id);
    }
    else{
      //get all cities
    }
  }

  saveRestaurant(){

  }

  createRestaurantTypesArray(){
    this.restaurantTypes.push({id:"1",value:"Pizzerie"}); 
    this.restaurantTypes.push({id:"2",value:"Bar/Pub"});
    this.restaurantTypes.push({id:"3",value:"Traditional romanesc"});
    this.restaurantTypes.push({id:"4",value:"Cafenea"});
    this.restaurantTypes.push({id:"5",value:"Restaurant italian"});
    this.restaurantTypes.push({id:"6",value:"Brasserie"});
    this.restaurantTypes.push({id:"7",value:"Berarie"});
    this.restaurantTypes.push({id:"8",value:"Rotiserie"});
    this.restaurantTypes.push({id:"9",value:"Ceainarie"});
    this.restaurantTypes.push({id:"10",value:"Restaurant italian"});
    this.restaurantTypes.push({id:"11",value:"Restaurant grecesc"});
    this.restaurantTypes.push({id:"12",value:"Restaurant chinezesc"});
    this.restaurantTypes.push({id:"13",value:"Restaurant libanez"});
    this.restaurantTypes.push({id:"14",value:"Restaurant turcesc"});
    this.restaurantTypes.push({id:"15",value:"Fast food"});
  }

  getRestaurantDetails(){
    //var userName = this.loggedUser.username;


    // this.restaurant = new Object();
    // this.restaurant.Name = "My rest";
    // this.restaurant.Description = "Restaurant description";
    // this.restaurant.Type = "Pizzerie";
    // this.restaurant.TypeId = "Pizzerie";
    // this.restaurant.County = "Dolj";
    // this.restaurant.City = "Craiova";
    // this.restaurant.CountiesAndCitiesId = "";
    // this.restaurant.Address = "Calea Bucuresti, 42";
    // this.restaurant.Contact = "Tel: 0722381894";
    // this.restaurant.ThumbnailBase64String = null;
    // this.restaurant.HasThumbnail = this.restaurant.ThumbnailBase64String != null;
    // this.restaurant.Program = "Luni-Vineri : 09-24; Sambata:09-22; Duminica : 09:20"
  }

  showError(text) {
    let alert = this.alertCtrl.create({
      title: 'Acces neautorizat',
      message: text,
      buttons: [
        {
          text: 'OK',
          handler: () => {
            this.navCtrl.push('LoginPage');
          }
        }
      ]
    });
    alert.present();
  }
}
