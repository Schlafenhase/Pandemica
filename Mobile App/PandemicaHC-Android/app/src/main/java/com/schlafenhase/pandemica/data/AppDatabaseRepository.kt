package com.schlafenhase.pandemica.data

import androidx.lifecycle.LiveData

// Declares the DAO as a private property in the constructor. Pass in the DAO
// instead of the whole database, because you only need access to the DAO
class AppDatabaseRepository(private val patientDAO: PatientDAO,
                            private val contactDAO: ContactDAO) {

    // Room executes all queries on a separate thread.
    // Observed LiveData will notify the observer when the data has changed.
    val allPatients: LiveData<List<Patient>> = patientDAO.getPatients()
    val allContacts: LiveData<List<Contact>> = contactDAO.getContacts()

    suspend fun deletePatient(patient: Patient) {
        patientDAO.deletePatient(patient)
    }

    suspend fun insertPatient(patient: Patient) {
        patientDAO.insert(patient)
    }

    suspend fun insertContact(contact: Contact) {
        contactDAO.insert(contact)
    }

}