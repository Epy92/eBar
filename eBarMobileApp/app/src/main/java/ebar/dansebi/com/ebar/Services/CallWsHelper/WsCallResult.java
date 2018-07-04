package ebar.dansebi.com.ebar.Services.CallWsHelper;

/**
 * Created by sebas on 3/14/2018.
 */

public class WsCallResult {
    private Object wsResponse;
    private String wsResponseJSON;
    private int callResult;

    public WsCallResult(){
        callResult = -1;
        wsResponse = null;
        wsResponseJSON = null;
    }

    public Object getWsResponse() {
        return wsResponse;
    }

    public void setWsResponse(Object wsResponse) {
        this.wsResponse = wsResponse;
    }

    public int getCallResult() {
        return callResult;
    }

    public void setCallResult(int callResult) {
        this.callResult = callResult;
    }

    public String getWsResponseJSON() {
        return wsResponseJSON;
    }

    public void setWsResponseJSON(String wsResponseJSON) {
        this.wsResponseJSON = wsResponseJSON;
    }
}
