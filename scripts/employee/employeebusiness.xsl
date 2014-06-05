<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('sys_businessemployee')">
          <businessemployee>
            <businessemployeeid m:left="-166" m:top="160">pk:businessemployee(<xsl:value-of select="businessid" />-<xsl:value-of select="employeeid" />)</businessemployeeid>
            <employeeid m:left="-42" m:top="176">fk:person(<xsl:value-of select="employeeid" />)</employeeid>
            <businessid m:left="-167" m:top="194">
              <xsl:value-of select="businessid" />
            </businessid>
            <isdefault m:left="-42" m:top="212">
              <xsl:value-of select="isdefault" />
            </isdefault>
            <mailmessageheader m:left="-166" m:top="229">
              <xsl:value-of select="mailmessageheader" />
            </mailmessageheader>
            <mailmessagefooter m:left="-40" m:top="247">
              <xsl:value-of select="mailmessagefooter" />
            </mailmessagefooter>
          </businessemployee>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>