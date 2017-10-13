namespace ITDB.Models.Custom
{
    public class response
    {
        ///状态
        public bool Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public response Success(string message){
            this.Status=true;
            this.Message=message;
            return this;
        }
        public response Success(dynamic data){
            this.Status=true;
            this.Message="请求成功";
            this.Data=data;
            return this;
        }
        public response Failure(string message){
            this.Status=false;
            this.Message=message;
            return this;
        }
        
    }
}