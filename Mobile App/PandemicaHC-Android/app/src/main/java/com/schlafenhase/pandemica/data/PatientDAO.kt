package com.schlafenhase.pandemica.data

import androidx.lifecycle.LiveData
import androidx.room.*

@Dao
interface PatientDAO {

    @Query("SELECT * from patient ORDER BY id ASC")
    fun getPatients(): LiveData<List<Patient>>

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun insert(patient: Patient)

    @Query("DELETE FROM patient")
    suspend fun deleteAll()

    @Delete
    suspend fun deletePatient(patient: Patient)
}