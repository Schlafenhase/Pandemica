package com.schlafenhase.pandemica.ui.home

import android.app.Application
import androidx.lifecycle.AndroidViewModel
import androidx.lifecycle.LiveData
import androidx.lifecycle.viewModelScope
import com.schlafenhase.pandemica.data.AppDatabase
import com.schlafenhase.pandemica.data.AppDatabaseRepository
import com.schlafenhase.pandemica.data.Patient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class HomeViewModel(application: Application) : AndroidViewModel(application) {

    private val repository: AppDatabaseRepository
    val allPatients: LiveData<List<Patient>>

    init {
        // Initialize app database and access DAO
        val patientDAO = AppDatabase.getDatabase(application, viewModelScope).patientDao()
        val contactDAO = AppDatabase.getDatabase(application, viewModelScope).contactDao()
        repository = AppDatabaseRepository(patientDAO, contactDAO)
        allPatients = repository.allPatients
    }

    /**
     * Launching a new coroutine to insert the data in a non-blocking way
     */
    fun insertPatient(patient: Patient) = viewModelScope.launch(Dispatchers.IO) {
        repository.insertPatient(patient)
    }

    /**
     * Launching a new coroutine to insert the data in a non-blocking way
     */
    fun delete(position: Int) = viewModelScope.launch(Dispatchers.IO) {
        val toDelete = allPatients.value?.get(position)
        if (toDelete != null) {
            repository.deletePatient(toDelete)
        }
    }
}