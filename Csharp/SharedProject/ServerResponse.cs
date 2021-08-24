using System;
using System.Collections.Generic;
using System.Text.Json;
using SharedProject.DTO;

namespace SharedProject
{
    [Serializable]
    public abstract class ServerResponse<T> where T : CommandDto
    {
        public string Status { get; set; } // TODO: make it as an enum

        public abstract byte[] ToByte();
    }

    [Serializable]
    public class ServerSimpleResponse<T> : ServerResponse<T> where T : CommandDto
    {
        public ServerSimpleResponse()
        {
            Status = null;
            Data = null;
        }

        public ServerSimpleResponse(T data)
        {
            Status = "success";
            Data = data;
        }

        public ServerSimpleResponse(string status, T data)
        {
            Status = status;
            Data = data;
        }

        public T Data { get; set; }


        public static ServerSimpleResponse<InfoDto> SuccessNoData()
        {
            return new ServerSimpleResponse<InfoDto>(null);
        }

        public override byte[] ToByte()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this);
        }
    }

    public abstract class ServerSimpleResponse : ServerSimpleResponse<InfoDto>
    {
    }

    [Serializable]
    public class ServerListResponse<T> : ServerResponse<T> where T : CommandDto
    {
        public ServerListResponse()
        {
            Status = null;
            Data = null;
        }

        public ServerListResponse(List<T> data)
        {
            Status = "success";
            Data = data;
        }

        public ServerListResponse(string status, List<T> data)
        {
            Status = status;
            Data = data;
        }

        public List<T> Data { get; set; }

        public override byte[] ToByte()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this);
        }
    }
}