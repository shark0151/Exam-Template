import axios,{
    AxiosResponse,
    AxiosError
} from "../../node_modules/axios/index"

interface Measure {
    id:number;
    temperature:number;
    humidity:number;
    pressure:number;
    timeStamp:Date; 
}

let WebApiUrl: string = "http://localhost:57666/api/Measurement";

let ContentElement: HTMLDivElement = <HTMLDivElement> document.getElementById("TableContent");
let GetAll_Btn: HTMLButtonElement = <HTMLButtonElement> document.getElementById("GetAll");
let GetById_Btn: HTMLButtonElement = <HTMLButtonElement> document.getElementById("GetById");
let DelById_Btn: HTMLButtonElement = <HTMLButtonElement> document.getElementById("DeleteById");
let AddNew_Btn: HTMLButtonElement = <HTMLButtonElement> document.getElementById("AddNew");


GetAll_Btn.addEventListener('click',GetAll);
GetById_Btn.addEventListener('click',GetById);
DelById_Btn.addEventListener('click',DeleteById);
AddNew_Btn.addEventListener('click',AddNewItem);

function GetAll():void
{
    ClearTable()
    axios.get<Measure[]>(WebApiUrl)
    .then(function (response: AxiosResponse<Measure[]>) : void
    {
        console.log("Data:");
        console.log(response);    
        response.data.forEach((thisItem:Measure) =>{AddToTable(thisItem)});
    })
    .catch(function (error:AxiosError) : void{
                    console.log("Error in the typescript code");
                    console.log(error);
                }
        )
    
    console.log("At the end of GetAll");
}

function GetById():void{

    ClearTable()
    let input = (<HTMLInputElement>document.getElementById("InputBar")).value;
    axios.get<Measure>(WebApiUrl+"/"+input)
    .then(function (response: AxiosResponse<Measure>) : void
    {
        console.log("Data:");
        console.log(response);
        let thisItem:Measure = response.data; 
        AddToTable(thisItem)
        
    })
    .catch(function (error:AxiosError) : void{
                    console.log("Error in the typescript code");
                    console.log(error);
                }
        )
    
    console.log("At the end of GetById function");
}

function AddNewItem():void{

    //let prop1 : number = Number((<HTMLInputElement>document.getElementById("prop1")).value);
    let prop2 : number = Number((<HTMLInputElement>document.getElementById("prop2")).value);
    let prop3 : number = Number((<HTMLInputElement>document.getElementById("prop3")).value);
    let prop4 : number = Number((<HTMLInputElement>document.getElementById("prop4")).value);
    let prop5 : Date = new Date(Date.now());

    axios.post<Measure>(WebApiUrl, { Id: 0, Temperature: prop2, Humidity: prop3, Pressure: prop4, TimeStamp: prop5})
    .then(function (response: AxiosResponse<Measure>) : void
    {
        console.log("Data:");
        console.log(response);    
        GetAll();
    })
    .catch(function (error:AxiosError) : void{
                    console.log("Error in the typescript code");
                    console.log(error);
                }
        )
    
    console.log("At the end of AddNewItem function");
}

function DeleteById():void{

    let input = (<HTMLInputElement>document.getElementById("InputBar")).value;
    axios.delete<Measure>(WebApiUrl+"/"+input)
    .then(function (response: AxiosResponse<Measure>) : void
    {
    console.log("Data:");
    console.log(response);    
    GetAll();
    })
    .catch(function (error:AxiosError) : void{
                    console.log("Error in the typescript code");
                    console.log(error);
                }
        )
    
    console.log("At the end of Delete function");
}

function ClearTable()
{
    //remove all the li elements one by one
    while (ContentElement.firstChild) {
            ContentElement.removeChild(ContentElement.lastChild);
        }
    //Set Headers
    var Row = document.createElement("tr");
            var node1 = document.createElement("td");
            var node2 = document.createElement("td");
            var node3 = document.createElement("td");
            var node4 = document.createElement("td");
            var node5 = document.createElement("td");
            node1.textContent = "Id";
            node2.textContent = "Temperature";
            node3.textContent = "Humidity";
            node4.textContent = "Pressure";
            node5.textContent = "TimeStamp";
            Row.appendChild(node1);
            Row.appendChild(node2);
            Row.appendChild(node3);
            Row.appendChild(node4); 
            Row.appendChild(node5); 
            ContentElement.appendChild(Row);
}

function AddToTable(Object:Measure)
{
    var Row = document.createElement("tr");
            var node1 = document.createElement("td");
            var node2 = document.createElement("td");
            var node3 = document.createElement("td");
            var node4 = document.createElement("td");
            var node5 = document.createElement("td");
            node1.textContent = Object.id.toString();
            node2.textContent = Object.temperature.toString();
            node3.textContent = Object.humidity.toString();
            node4.textContent = Object.pressure.toString();
            node5.textContent = Object.timeStamp.toString();
            Row.appendChild(node1);
            Row.appendChild(node2);
            Row.appendChild(node3);
            Row.appendChild(node4); 
            Row.appendChild(node5); 
            ContentElement.appendChild(Row);
}
