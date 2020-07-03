package com.schlafenhase.pandemica.ui.settings

import android.app.Application
import androidx.lifecycle.AndroidViewModel
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import com.google.firebase.auth.FirebaseAuth

class SettingsViewModel(application: Application) : AndroidViewModel(application) {
    var fbAuth = FirebaseAuth.getInstance()
    val name = "MP"
    val location = "Grecia"
    var email = "apple"
    val hcID = "42"
    val bedCount = "23"
    val ICUCount = "23"
    val director = "23"
    val contact = "23"

    private val _hcInfo = MutableLiveData<String>().apply {
        value = "Name: $name at $location \nEmail: $email \nHealth Center ID: $hcID \nBed Count: $bedCount (ICU: $ICUCount) \nDirector: $director (Contact: $contact)"
    }
    val hcInfo: LiveData<String> = _hcInfo

    init {
        updateInfo()
    }

    /**
     * Updates Health Center label info on first sign in
     */
    fun updateInfo() {
        email = fbAuth.currentUser?.email.toString()
        // CONNECT TO API AND UPDATE LABELS
        _hcInfo.value = "Name: $name at $location \nEmail: $email \nHealth Center ID: $hcID \nBed Count: $bedCount (ICU: $ICUCount) \nDirector: $director (Contact: $contact)"
    }
}