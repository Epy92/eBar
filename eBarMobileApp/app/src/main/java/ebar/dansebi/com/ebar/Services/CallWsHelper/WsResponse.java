package ebar.dansebi.com.ebar.Services.CallWsHelper;

import com.google.gson.annotations.SerializedName;

/**
 * Created by sebas on 3/4/2018.
 */

public class WsResponse {

    @SerializedName("SessionKey")
    private String SessionKey;
    @SerializedName("ResultCode")
    private String ResultCode;
    @SerializedName("ResultFlag")
    private boolean ResultFlag;
    @SerializedName("ResultMessage")
    private String ResultMessage;

    public WsResponse(String sessionKey, String resultCode, boolean resultFlag, String resultMessage) {
        SessionKey = sessionKey;
        ResultCode = resultCode;
        ResultFlag = resultFlag;
        ResultMessage = resultMessage;
    }

    public String getResultMessage() {
        return ResultMessage;
    }

    public void setResultMessage(String resultMessage) {
        ResultMessage = resultMessage;
    }

    public boolean isResultFlag() {
        return ResultFlag;
    }

    public void setResultFlag(boolean resultFlag) {
        ResultFlag = resultFlag;
    }

    public String getResultCode() {
        return ResultCode;
    }

    public void setResultCode(String resultCode) {
        ResultCode = resultCode;
    }

    public String getSessionKey() {
        return SessionKey;
    }

    public void setSessionKey(String sessionKey) {
        SessionKey = sessionKey;
    }
}
